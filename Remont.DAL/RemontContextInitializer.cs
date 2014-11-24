using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Remont.Common.Model;

namespace Remont.DAL
{
    public class RemontContextInitializer : DropCreateDatabaseIfModelChanges<RemontContext>
    {
        protected override void Seed(RemontContext context)
        {
            context.Controls.Add(new Control
            {
                ControlId = "TEXT"
            });

            context.Controls.Add(new Control
            {
                ControlId = "SELECT"
            });

			context.Controls.Add(new Control
			{
				ControlId = "PEOPLE_PICKER"
			});

            base.Seed(context);
        }
    }
}
