using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Commerce.Plugin.ExtendCatalog.Components;
using Sitecore.Commerce.Plugin.ExtendCatalog.Policies;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
namespace Sitecore.Commerce.Plugin.ExtendCatalog.Pipelines.Blocks.DoActions
{
    [PipelineDisplayName(NotesConstants.Pipelines.Blocks.DoActionEditNotesBlock)]
    public class DoActionEditNotesBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        private readonly CommerceCommander _commerceCommander;
        public DoActionEditNotesBlock(CommerceCommander commerceCommander)
        {
            _commerceCommander = commerceCommander;
        }
        public override Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{Name}: The argument cannot be null.");

            var notesActionsPolicy = context.GetPolicy<KnownNotesActionsPolicy>();

            // Only proceed if the right action was invoked
            if (string.IsNullOrEmpty(arg.Action) || !arg.Action.Equals(notesActionsPolicy.EditNotes, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(arg);
            }

            // Get the sellable item from the context
            var entity = context.CommerceContext.GetObject<SellableItem>(x => x.Id.Equals(arg.EntityId));
            if (entity == null)
            {
                return Task.FromResult(arg);
            }

            // Get the notes component from the sellable item or its variation
            var component = entity.GetComponent<NotesComponents>(arg.ItemId);

            // Map entity view properties to component
            component.WarrantyInformation =
                arg.Properties.FirstOrDefault(x =>
                        x.Name.Equals(nameof(NotesComponents.WarrantyInformation), StringComparison.OrdinalIgnoreCase))?.Value;

            component.InternalNotes =
                arg.Properties.FirstOrDefault(x =>
                    x.Name.Equals(nameof(NotesComponents.InternalNotes), StringComparison.OrdinalIgnoreCase))?.Value;

            // Persist changes
            this._commerceCommander.Pipeline<IPersistEntityPipeline>().Run(new PersistEntityArgument(entity), context);

            return Task.FromResult(arg);
        }
    }
}
