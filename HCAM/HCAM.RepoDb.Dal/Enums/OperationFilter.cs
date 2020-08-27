using System;
using System.Collections.Generic;
using System.Text;

namespace HCAM.RepoDb.Dal.Enums
{
    public enum OperationFilter
    {
        /// <summary>
        /// An equal operation.
        /// </summary>
        Equal,
        /// <summary>
        /// A not-equal operation.
        /// </summary>
         NotEqual,
        /// <summary>
        /// A less-than operation.
        /// </summary>
        LessThan,
        /// <summary>
        /// A greater-than operation.
        /// </summary>
        GreaterThan,
        /// <summary>
        /// A less-than-or-equal operation.
        /// </summary>
        LessThanOrEqual,
        /// <summary>
        /// A greater-than-or-equal operation.
        /// </summary>
        GreaterThanOrEqual,
        /// <summary>
        /// A like operation. Defines the <i>LIKE</i> keyword in SQL Statement.
        /// </summary>
        Like,
        /// <summary>
        /// A not-like operation. Defines the <i>NOT LIKE</i> keyword in SQL Statement.
        /// </summary>
        NotLike,
        /// <summary>
        /// A between operation. Defines the <i>BETWEEN</i> keyword in SQL Statement.
        /// </summary>
        Between,
        /// <summary>
        /// A not-between operation. Defines the <i>NOT BETWEEN</i> keyword in SQL Statement.
        /// </summary>
        NotBetween,
        /// <summary>
        /// An in operation. Defines the <i>IN</i> keyword in SQL Statement.
        /// </summary>
        In,
        /// <summary>
        /// A non-in operation. Defines the <i>NOT IN</i> keyword in SQL Statement.
        /// </summary>
        NotIn,
        /// <summary>
        /// An (AND) equation and operation. This defines that all query expression must be true.
        /// </summary>
        All,
        /// <summary>
        /// An (OR) equation and operation. This defines that any query expression must be true.
        /// </summary>
        Any
    }
}
