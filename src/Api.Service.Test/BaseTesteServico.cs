using System;
using Xunit;
using AutoMapper;
using Api.CrossCutting.Mappings;

namespace Api.Service.Test
{
    public abstract class BaseTesteServico
    {       
        public IMapper Mapper {get; set;}    

        public BaseTesteServico()
        {

        }
        
        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {
                var config = new AutoMapper.MapperConfiguration
                (
                    cfg => 
                    {
                        cfg.AddProfile(new DtoToModelProfile());
                        cfg.AddProfile(new ModelToEntityProfile());
                        cfg.AddProfile(new EntityToDtoProfile());
                    }    
                );
                
            }

            public void Dispose()
            {
                
            }
        }
    }

}
