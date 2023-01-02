using System;

namespace FF3Manip
{
    public class ManipList
    {
        private static ManipList _ManipList = null;
        public static ManipList Instance
        {
            get
            {
                if(_ManipList == null)
                {
                    _ManipList = new ManipList();
                }
                return _ManipList;
            }
        }

        public void AltarCave()
        {
            Console.Clear();
            TimeZone.Instance.SetTimeZone(TimeZones.ET);
            Manip.Instance.SetDateTime(new Manip.TargetDateTime(13, 10, 2022, 21, 20, 31));
        }

        public void LandTurtle()
        {
            Console.Clear();
            TimeZone.Instance.SetTimeZone(TimeZones.ET);
            Manip.Instance.SetDateTime(new Manip.TargetDateTime(16, 10, 2022, 16, 37, 01));
        }
    }
}
