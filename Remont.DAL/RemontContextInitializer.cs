using System.Data.Entity;
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
				ControlId = "ENTITY_PICKER"
			});

			context.Controls.Add(new Control
			{
				ControlId = "LIST_ENTITY_PICKER"
			});

            base.Seed(context);
        }
    }
}
