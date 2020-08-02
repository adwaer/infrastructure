﻿namespace In.DDD
{
    public interface IAggregateRoot
    {
        
    }
    public interface IAggregateRoot<TModel>: IAggregateRoot
    {
        TModel Model { get; }
        bool IsNew { get; }
        void SetModel(TModel entity, bool isNew);
    }
}