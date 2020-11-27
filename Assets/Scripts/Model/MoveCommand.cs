using System;

namespace Model
{
    /// <summary>
    /// 시뮬레이션에 물품을 이동하는 명령을 내린다.
    /// </summary>
    [Serializable]
    public class MoveCommand
    {
        public int slot_id;
        public double row;
        public double column;
        public double depth;
    }
}