using System;

namespace Bridge.Concrete._1
{
    class Program
    {
        static void Main(string[] args)
        {
            IDevice tv = new Tv();
            Remote remote = new CommonRemote(tv);
            remote.TogglePower();
            remote.ChannelUp();

            IDevice radio = new Radio();
            remote.Device = radio;
            remote.ChannelUp();

            Console.Read();
        }
    }

    interface IDevice
    {
        bool IsEnabled();
        void Enable();
        void Disable();
        int GetVolume();
        void SetVolume(int percent);
        int GetChannel();
        void SetChannel(int channel);
    }

    public class Tv : IDevice
    {
        private bool isEnabled = false;
        private int channel = 20;

        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void Enable()
        {
            isEnabled = true;
            Console.WriteLine("Tv has been enable");
        }

        public int GetChannel()
        {
            return channel;
        }

        public int GetVolume()
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled()
        {
            return isEnabled;
        }

        public void SetChannel(int channel)
        {
            this.channel = channel;
            Console.WriteLine("TV channel has been set to {0}", channel);
        }

        public void SetVolume(int percent)
        {
            throw new NotImplementedException();
        }
    }

    public class Radio : IDevice
    {
        private int channel = 10;

        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void Enable()
        {
            throw new NotImplementedException();
        }

        public int GetChannel()
        {
            return channel;
        }

        public int GetVolume()
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled()
        {
            throw new NotImplementedException();
        }

        public void SetChannel(int channel)
        {
            this.channel = channel;
            Console.WriteLine("Radio channel has been set to {0}", channel);
        }

        public void SetVolume(int percent)
        {
            throw new NotImplementedException();
        }
    }

    abstract class Remote
    {
        protected IDevice device;
        public IDevice Device
        {
            set { device = value; }
        }
        public Remote(IDevice device)
        {
            this.device = device;
        }
        public virtual void TogglePower()
        {
            if (device.IsEnabled())
            {
                device.Disable();
            }
            else
            {
                device.Enable();
            }
        }
        public virtual void VolumeDown()
        {
            device.SetVolume(device.GetVolume() - 10);
        }
        public virtual void VolumeUp()
        {
            device.SetVolume(device.GetVolume() + 10);
        }
        public virtual void ChannelDown()
        {
            device.SetChannel(device.GetChannel() - 1);
        }
        public virtual void ChannelUp()
        {
            device.SetChannel(device.GetChannel() + 1);
        }
    }

    class CommonRemote : Remote
    {
        public CommonRemote(IDevice lang) : base(lang)
        {
        }

    }

    class AdvancedRemote : Remote
    {
        public AdvancedRemote(IDevice lang) : base(lang)
        {
        }
        public void Mute()
        {
            device.SetVolume(0);
        }
    }
}
