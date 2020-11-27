using System;

namespace Model
{
    /// <summary>
    /// 지우는 명령을 내린다.
    /// </summary>
    [Serializable]
    public class DeleteCommand
    {
        public int slot_id;
    }
}