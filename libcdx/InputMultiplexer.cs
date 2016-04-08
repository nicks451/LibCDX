using System;
using System.Collections.Generic;

namespace libcdx
{
    public class InputMultiplexer : IInputProcessor
    {
        private List<IInputProcessor> processors = new List<IInputProcessor>();

        public InputMultiplexer()
        {
        }

        public InputMultiplexer(params IInputProcessor[] processors)
        {
            for (int i = 0; i < processors.Length; i++)
            {
                this.processors.Add(processors[i]);
            }
        }

        public void AddProcessor(int index, IInputProcessor processor)
        {
            if (processor == null)
            {
                throw new NullReferenceException("processor cannot be null");
            }
            processors.Insert(index, processor);
        }

        public void RemoveProcessor(int index)
        {
            processors.RemoveAt(index);
        }

        public void AddProcessor(IInputProcessor processor)
        {
            if (processor == null)
            {
                throw new NullReferenceException("processor cannot be null");
            }
            processors.Add(processor);
        }

        public void RemoveProcessor(IInputProcessor processor)
        {
            processors.Remove(processor);
        }

        /** @return the number of processors in this multiplexer */
        public int Size()
        {
            return processors.Count;
        }

        public void Cclear()
        {
            processors.Clear();
        }

        public void SetProcessors(List<IInputProcessor> processors)
        {
            this.processors = processors;
        }

        public List<IInputProcessor> GetProcessors()
        {
            return processors;
        }

        public bool KeyDown(int keycode)
        {
            for (int i = 0, n = processors.Count; i < n; i++)
            {
                if (processors[i].KeyDown(keycode))
                {
                    return true;
                }
            }
            return false;
        }

        public bool KeyUp(int keycode)
        {
            for (int i = 0, n = processors.Count; i < n; i++)
            {
                if (processors[i].KeyUp(keycode))
                {
                    return true;
                }
            }
            return false;
        }

        public bool KeyTyped(char character)
        {
            for (int i = 0, n = processors.Count; i < n; i++)
            {
                if (processors[i].KeyTyped(character))
                {
                    return true;
                }
            }
            return false;
        }

        public bool TouchDown(int screenX, int screenY, int pointer, int button)
        {
            for (int i = 0, n = processors.Count; i < n; i++)
            {
                if (processors[i].TouchDown(screenX, screenY, pointer, button))
                {
                    return true;
                }
            }
            return false;
        }

        public bool TouchUp(int screenX, int screenY, int pointer, int button)
        {
            for (int i = 0, n = processors.Count; i < n; i++)
            {
                if (processors[i].TouchUp(screenX, screenY, pointer, button))
                {
                    return true;
                }
            }
            return false;
        }

        public bool TouchDragged(int screenX, int screenY, int pointer)
        {
            for (int i = 0, n = processors.Count; i < n; i++)
                if (processors[i].TouchDragged(screenX, screenY, pointer))
                {
                    return true;
                }
            return false;
        }

        public bool MouseMoved(int screenX, int screenY)
        {
            for (int i = 0, n = processors.Count; i < n; i++)
            {
                if (processors[i].MouseMoved(screenX, screenY))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Scrolled(int amount)
        {
            for (int i = 0, n = processors.Count; i < n; i++)
            {
                if (processors[i].Scrolled(amount))
                {
                    return true;
                }
            }
            return false;
        }
    }
}