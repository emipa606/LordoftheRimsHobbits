using RimWorld;
using Verse;

namespace Hobbits
{
    [DefOf]
    public static class ThoughtDefOf
    {
        static ThoughtDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ThoughtDefOf));
        }

        public static ThoughtDef ObservedPartyTree;
    }
    public class Plant_PartyTree : Plant, IThoughtGiver
    {
        public override float Growth
        {
            get => base.Growth;
            set
            {
                base.Growth = value;
                CompGlower compGlower = this.TryGetComp<CompGlower>();
                compGlower.UpdateLit(base.Map);
            }
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            CompGlower compGlower = this.TryGetComp<CompGlower>();
            compGlower.UpdateLit(map);
        }
        public override void DeSpawn(DestroyMode mode = DestroyMode.Vanish)
        {
            Map map = base.Map;
            base.DeSpawn(mode);
            CompGlower compGlower = this.TryGetComp<CompGlower>();
            compGlower.UpdateLit(map);
        }
        public override void PostMapInit()
        {
            base.PostMapInit();
            CompGlower compGlower = this.TryGetComp<CompGlower>();
            compGlower.UpdateLit(base.Map);
        }
        public Thought_Memory GiveObservedThought()
        {
            if (LifeStage != PlantLifeStage.Mature)
            {
                return null;
            }

            var thought_MemoryObservation = (Thought_MemoryObservation)ThoughtMaker.MakeThought(Hobbits.ThoughtDefOf.ObservedPartyTree);
            thought_MemoryObservation.Target = this;
            return thought_MemoryObservation;
        }
    }
}
