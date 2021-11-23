using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPT_WPF
{
    public class cd_Profile
    {
        #region 프로파일값 가져오기
        public Dictionary<string, string> SplitProfile(string contentProfile)
        {
            string _contentProfile = "";
            _contentProfile = contentProfile;

            List<string> _splited = new List<string>();
            _splited = _contentProfile.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var dict = new Dictionary<string, string>();

            foreach (string ff in _splited)
            {
                if (ff.Contains("[M") || ff.Contains("[g") ||  ff.Contains("\r"))
                {

                }
                else
                {
                    List<string> _lines = new List<string>();
                    _lines = ff.Split(new string[] { " : " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    dict.Add(_lines[0], _lines[1]);
                }
            }
            return dict;
        }


        #endregion
    }
}
