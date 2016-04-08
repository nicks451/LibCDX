using System;
using System.IO;
using System.Text;
using libcdx.Utils;

namespace libcdx.Files
{
    public class FileHandle
    {
        protected FileInfo File;
        protected FileType Type { get; }

        protected FileHandle()
        {
            
        }

        public FileHandle(string fileName)
        {
            this.File = new FileInfo(fileName);
            this.Type = FileType.Absolute;
        }

        public FileHandle(FileInfo file)
        {
            this.File = file;
            this.Type = FileType.Absolute;
        }

        public FileHandle(string fileName, FileType type)
        {
            this.File = new FileInfo(fileName);
            this.Type = type;
        }

        public string Path()
        {
            return this.File.FullName;
        }

        public string Name()
        {
            return this.File.Name;
        }

        public string Extension()
        {
            return this.File.Extension;
        }

        public string NameWithoutExtension()
        {
            string fileName = this.File.Name;

            int dotIndex = fileName.LastIndexOf(".", StringComparison.Ordinal);

            if (dotIndex == -1)
            {
                return fileName;
            }

            return fileName.Substring(0, dotIndex);
        }

        public String PathWithoutExtension()
        {
            string filePath = this.File.FullName;

            int dotIndex = filePath.LastIndexOf(".", StringComparison.Ordinal);

            if (dotIndex == -1)
            {
                return filePath;
            }

            return filePath.Substring(0, dotIndex);
        }

        public FileStream Read()
        {
            if (this.Type == FileType.Classpath || (this.Type == FileType.Internal && !this.File.Exists)
                || (this.Type == FileType.Local && !this.File.Exists))
            {
                try
                {
                    FileStream input = File.OpenRead();

                    return input;
                }
                catch (Exception)
                {
                    throw new CdxRuntimeException("File not found: " + File.Name + " (" + Type + ")");
                }
            }

            return File.OpenRead();
        }

        public BufferedStream Read(int bufferSize)
        {
            return new BufferedStream(File.OpenRead(), bufferSize);
        }

        public StreamReader Reader()
        {
            return new StreamReader(Read());
        }

        public StreamReader Reader(Encoding charset)
        {
            FileStream stream = Read();
            try
            {
                return new StreamReader(stream, charset);
            }
            catch (Exception ex)
            {
                stream.Close();
                throw new CdxRuntimeException("Error reading file: " + this, ex);
            }
        }

        public BufferedStream Reader(int bufferSize)
        {
            return Read(bufferSize);
        }

        public BufferedStream Reader(int bufferSize, Encoding charset)
        {
            //TODO figure out implementation
            return Reader(bufferSize);
        }

        public string ReadString()
        {
            return ReadString(null);
        }

        public string ReadString(Encoding charset)
        {
            StringBuilder output = new StringBuilder(EstimateLength());
            StreamReader reader = null;

            try
            {
                if (charset == null)
                {
                    reader = new StreamReader(Read());
                }
                else
                {
                    reader = new StreamReader(Read(), charset);
                }

                output.Append(reader.ReadToEnd());
            }
            catch (Exception ex)
            {
                throw new CdxRuntimeException("Error reading layout file: " + this, ex);
            }
            finally
            {
                reader?.Close();
            }

            return output.ToString();
        }

        public byte[] ReadBytes()
        {
            FileStream input = Read();
            try
            {
                byte[] buffer = new byte[16*1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read = 0;
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }

                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new CdxRuntimeException("Error reading file: " + this, ex);
            }
            finally
            {
                input.Close();
            }
        }

        private int EstimateLength()
        {
            int length = (int) Length();

            return length != 0 ? length : 512;
        }

        public long Length()
        {
            if (Type == FileType.Classpath || (Type == FileType.Internal && !File.Exists))
            {
                FileStream input = Read();
                try
                {
                    return input.Length;
                }
                catch (Exception)
                {
                }
                finally
                {
                    input.Close();
                }
                return 0;
            }
            return File.Length;
        }

        public int ReadBytes(byte[] bytes, int offset, int size)
        {
            FileStream input = Read();
            int position = 0;

            try
            {
                while (true)
                {
                    int count = input.Read(bytes, offset + position, size - position);
                    if (count <= 0)
                    {
                        break;
                    }
                    position += count;
                }
            }
            catch (Exception ex)
            {
                throw new CdxRuntimeException("Error Reading File: " + this, ex);
            }
            finally
            {
                input.Close();
            }

            return position - offset;
        }

        public FileStream Write(bool append)
        {
            if (Type == FileType.Classpath)
            {
                throw new CdxRuntimeException("Cannot write to a classpath file: " + File);
            }
            if (Type == FileType.Internal)
            {
                throw new CdxRuntimeException("Cannot write to an internal file: " + File);
            }

            try
            {
                return System.IO.File.OpenWrite(File.FullName);
            }
            catch (Exception ex)
            {
                throw new CdxRuntimeException("Error Writing File: " + File + " (" + Type + ")", ex);
            }
        }

        public BufferedStream Write(bool append, int bufferSize)
        {
            FileStream stream = Write(append);
            return new BufferedStream(stream, bufferSize);
        }

        public void Write(FileStream input, bool append)
        {
            FileStream output = null;
            try
            {
                output = Write(append);

                output.CopyTo(input);
            }
            catch (Exception ex)
            {
                throw new CdxRuntimeException("Error stream writing to file: " + File + " (" + Type + ")", ex);
            }
            finally
            {
                output?.Close();
                input.Close();
            }
        }
        
    }
}