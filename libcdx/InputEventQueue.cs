using System.Runtime.CompilerServices;
using libcdx.Utils;

namespace libcdx
{
    public class InputEventQueue : IInputProcessor
    {
        private const int KEY_DOWN = 0;
        private const int KEY_UP = 1;
        private const int KEY_TYPED = 2;
        private const int TOUCH_DOWN = 3;
        private const int TOUCH_UP = 4;
        private const int TOUCH_DRAGGED = 5;
        private const int MOUSE_MOVED = 6;
        private const int SCROLLED = 7;

        private IInputProcessor processor;
        private readonly IntArray queue = new IntArray();
        private readonly IntArray processingQueue = new IntArray();
        private long currentEventTime;

        public InputEventQueue()
        {
        }

        public InputEventQueue(IInputProcessor processor)
        {
            this.processor = processor;
        }

        public void setProcessor(IInputProcessor processor)
        {
            this.processor = processor;
        }

        public IInputProcessor getProcessor()
        {
            return processor;
        }

        public void Drain()
        {
            IntArray q = processingQueue;
            
            if (processor == null)
            {
                queue.Clear();
                return;
            }
            q.AddAll(queue);
            queue.Clear();

            IInputProcessor localProcessor = processor;
            for (int i = 0, n = q.size; i < n;)
            {
                currentEventTime = (long)q.Get(i++) << 32 | q.Get(i++) & 0xFFFFFFFFL;
                switch (q.Get(i++))
                {
                    case KEY_DOWN:
                        localProcessor.KeyDown(q.Get(i++));
                        break;
                    case KEY_UP:
                        localProcessor.KeyUp(q.Get(i++));
                        break;
                    case KEY_TYPED:
                        localProcessor.KeyTyped((char)q.Get(i++));
                        break;
                    case TOUCH_DOWN:
                        localProcessor.TouchDown(q.Get(i++), q.Get(i++), q.Get(i++), q.Get(i++));
                        break;
                    case TOUCH_UP:
                        localProcessor.TouchUp(q.Get(i++), q.Get(i++), q.Get(i++), q.Get(i++));
                        break;
                    case TOUCH_DRAGGED:
                        localProcessor.TouchDragged(q.Get(i++), q.Get(i++), q.Get(i++));
                        break;
                    case MOUSE_MOVED:
                        localProcessor.MouseMoved(q.Get(i++), q.Get(i++));
                        break;
                    case SCROLLED:
                        localProcessor.Scrolled(q.Get(i++));
                        break;
                }
            }
            q.Clear();
        }

        private void QueueTime()
        {
            long time = TimeUtils.NanoTime();
            queue.add((int)(time >> 32));
            queue.add((int)time);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool KeyDown(int keycode)
        {
            QueueTime();
            queue.add(KEY_DOWN);
            queue.add(keycode);
            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool KeyUp(int keycode)
        {
            QueueTime();
            queue.add(KEY_UP);
            queue.add(keycode);
            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool KeyTyped(char character)
        {
            QueueTime();
            queue.add(KEY_TYPED);
            queue.add(character);
            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool TouchDown(int screenX, int screenY, int pointer, int button)
        {
            QueueTime();
            queue.add(TOUCH_DOWN);
            queue.add(screenX);
            queue.add(screenY);
            queue.add(pointer);
            queue.add(button);
            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool TouchUp(int screenX, int screenY, int pointer, int button)
        {
            QueueTime();
            queue.add(TOUCH_UP);
            queue.add(screenX);
            queue.add(screenY);
            queue.add(pointer);
            queue.add(button);
            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool TouchDragged(int screenX, int screenY, int pointer)
        {
            QueueTime();
            queue.add(TOUCH_DRAGGED);
            queue.add(screenX);
            queue.add(screenY);
            queue.add(pointer);
            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool MouseMoved(int screenX, int screenY)
        {
            QueueTime();
            queue.add(MOUSE_MOVED);
            queue.add(screenX);
            queue.add(screenY);
            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool Scrolled(int amount)
        {
            QueueTime();
            queue.add(SCROLLED);
            queue.add(amount);
            return false;
        }

        public long GetCurrentEventTime()
        {
            return currentEventTime;
        }
    }
}