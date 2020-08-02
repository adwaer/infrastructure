﻿namespace In.DDD.Implementations
{
    public class SimpleIAggregateRoot<TModel> : IAggregateRoot<TModel>
    {
        public void SetModel(TModel entity, bool isNew)
        {
            Model = entity;
            IsNew = isNew;
        }

        public bool IsNew { get; private set; }

        public TModel Model { get; private set; }
    }
}