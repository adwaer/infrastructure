using In.Di;

namespace In.Mapping
{
    public class MapperServiceFactory
    {
        private readonly IDiScope _diContainer;

        public MapperServiceFactory(IDiScope diContainer)
        {
            _diContainer = diContainer;
        }

        public TDest GetFrom<TDest, TDto>(TDto model)
        {
            var mapperService = _diContainer.Resolve<IMapperService<TDest, TDto>>();
            return mapperService.GetFrom(model);
        }
    }
}
