using System;

namespace Project.Code.Infrastructure.Services.UpdateBehavior
{
    public interface IUpdateBehaviourService
    {
        event Action UpdateEvent;
        event Action FixedUpdateEvent;
    }
}