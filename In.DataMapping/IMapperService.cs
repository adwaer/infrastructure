namespace In.DataMapping
{
    public interface IMapperService<out TDest, in TDto>
    {
        TDest GetFrom(TDto model, object mappingData = null);
    }
}