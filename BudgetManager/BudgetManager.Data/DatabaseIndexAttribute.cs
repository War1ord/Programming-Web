using System;

namespace BudgetManager.Attributes
{
    /// <summary>
    /// Index Filter to helper adding indexes
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class DatabaseIndexAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseIndexAttribute"/> class.
        /// </summary>
        /// <param name="name">Index name.</param>
        /// <param name="unique"><c>true</c> if this instance is a unique index; otherwise, <c>false</c>.</param>
        public DatabaseIndexAttribute(string name, bool unique = false)
        {
            this.Name = name;
            this.IsUnique = unique;
        }

        /// <summary>
        /// Get the name of the index
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is a unique index or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is a unique index; otherwise, <c>false</c>.
        /// </value>
        public bool IsUnique { get; private set; }
    }
}
