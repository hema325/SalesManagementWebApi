namespace Domain.Common.Events
{
    public class EntityDeletedEvent<TEntity>: EventBase
    {
        public TEntity Entity { get; }

        public EntityDeletedEvent(TEntity entity)
        {
            Entity = entity;
        }
    }
}
