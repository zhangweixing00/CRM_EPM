using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlQuadrant
{
    /// <summary>
    /// 评价
    /// </summary>
    [Serializable]
    public class EvaluateInfo
    {
        /// <summary>
        /// 象限ID
        /// </summary>
        public int QId { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }
    }
}
