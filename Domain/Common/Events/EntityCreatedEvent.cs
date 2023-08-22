namespace Domain.Common.Events
{
    public class EntityCreatedEvent<TEntity>: EventBase
    {
        public TEntity Entity { get; }

        public EntityCreatedEvent(TEntity entity)
        {
            Entity = entity;
        }

    }
}
