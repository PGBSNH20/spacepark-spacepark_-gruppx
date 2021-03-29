using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleApp
{
    interface IStarshipResponse
    {
        Task<StarshipResponse> GetShips();

    }
}
