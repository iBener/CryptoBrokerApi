using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.Util.Mappings;

public interface IMapFrom<T>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
        profile.CreateMap(GetType(), typeof(T));
    }
}
