namespace Domain.Common.Events
{
    public class EntityUpdatedEvent<TEntity>: EventBase
    {
        public TEntity Entity { get; }

        public EntityUpdatedEvent(TEntity entity)
        {
            Entity = entity;
        }
    }
}
