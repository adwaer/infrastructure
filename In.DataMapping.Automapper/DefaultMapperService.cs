using AutoMapper;

namespace In.DataMapping.Automapper
{
    public class DefaultMapperService : IMapperService
    {
        private readonly IMapper _mapper;

        public DefaultMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDest GetFrom<TDest, TFrom>(TFrom model)
        {
            return _mapper.Map<TDest>(model);
        }
    }
}