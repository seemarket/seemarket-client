using System;

namespace Model
{
    /// <summary>
    /// 시뮬레이션에 물품을 추가하는 명령을 내린다.
    /// </summary>
    [Serializable]
    public class CreateCommand
    {
        public int product_id;
        public double row;
        public double column;
        public double depth;
    }

}