using System.Collections.Generic;

namespace DefaultNamespace.ARImage
{
    public class ARImageLinker
    {
        private Dictionary<string, string> info;

        public string getname(string id)
        {
            return info[id];
        }

        public void setupDatabase()
        {
            info = new Dictionary<string, string>();

            info["1"] = "칠성사이다";
            info["2"] = "펩시콜라";
            info["3"] = "밀키스";
            info["4"] = "핫식스";
            info["5"] = "레스비";
            info["6"] = "2%";
            info["7"] = "코카콜라";
            info["8"] = "환타";
            info["9"] = "파워에이드";
            info["10"] = "슈웹스";
            info["11"] = "토레타";
            info["12"] = "스프라이트";
            info["13"] = "미닛메이드";
            info["14"] = "써니텐";
            info["15"] = "조지아";
            info["drpepper"] = "닥터페퍼";
            info["mountaindew"] = "마운틴듀";
            info["redbull"] = "레드불";
        }
    }
}