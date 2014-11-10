
using System.Collections;
using System.Collections.Generic;

namespace Remont.Common.Model
{
    public class Customer : BaseItem<int>
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string Email { get; set; }

        public virtual IList<Order> Orders { get; set; }
    }
}
