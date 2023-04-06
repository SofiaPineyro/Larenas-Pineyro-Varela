using System.Collections.Generic;

namespace ArenaGestor.Domain
{
    public class ConcertsInsertResult
    {
        public int InsertedRecords { get; set; }
        public int NotInsertedRecords { get; set; }
        public List<string> Messages { get; set; }
        public ConcertsInsertResult()
        {
            this.Messages = new List<string>();
        }
    }
}
