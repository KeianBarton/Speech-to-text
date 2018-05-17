using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services.Models;

namespace Services.IServices
{
    public interface IAmazonUploader
    {
        Task<bool> Tester();
    }
}
