using System;
using System.Collections.Generic;
using System.Text;

namespace ETO
{
    [Serializable]
    public class ProductStockCountChangedEto //: EtoBase
    {
        public Guid Id { get; }

        public int OldCount { get; set; }

        public int CurrentCount { get; set; }

        private ProductStockCountChangedEto()
        {
            //Default constructor is needed for deserialization.
        }

        public ProductStockCountChangedEto(Guid id, int oldCount, int currentCount)
        {
            Id = id;
            OldCount = oldCount;
            CurrentCount = currentCount;
        }
    }
}
