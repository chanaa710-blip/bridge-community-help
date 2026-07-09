using AutoMapper;
using Bridge.Core.Models;
using Bridge.Core.Repository;
using Bridge.Core.Resources;
using Bridge.Core.Services;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Service
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public RequestService(IRequestRepository requestRepository, IMapper mapper, IUserRepository userRepository, ICategoryRepository categoryRepository) 
        {
            _requestRepository = requestRepository; 
            _mapper = mapper;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<Guid> Add(RequestResource entity)
        {
            var userExists = await _userRepository.GetById(entity.UserId);
            if (userExists == null)
                throw new Exception("The user providing this request does not exist.");

            var categoryExists = await _categoryRepository.GetById(entity.CategoryId);
            if (categoryExists == null)
                throw new Exception("The category provided does not exist.");

            var request = _mapper.Map<Request>(entity);
            return await _requestRepository.Add(request);
        }

        public async Task<IEnumerable<RequestResource>> GetAll()
        {
            var requests = await _requestRepository.GetAll();
            return _mapper.Map<IEnumerable<RequestResource>>(requests);
        }
        public Task<int> DeleteById(Guid id)
        {
            return (_requestRepository.DeleteById(id));
        }

        public async Task<RequestResource?> GetById(Guid id)
        {
           var request= await _requestRepository.GetById(id);
            return _mapper.Map<RequestResource>(request);
        }

        public async Task<IEnumerable<RequestResource>> GetRequestsByLocationAndCategory(double lat, double lng, double radiusInMeters, Guid? categoryId)
        {
            var centerPoint = new Point(lng, lat)
            {
                SRID = 4326
            };
            var requests = (await _requestRepository.GetRequestsInRadius(centerPoint, radiusInMeters))
                .AsEnumerable();

            if (categoryId.HasValue)
            {
                requests = requests.Where(r => r.CategoryId == categoryId.Value);
            }

            return _mapper.Map<IEnumerable<RequestResource>>(requests);
        }

        public Task<int> Update(RequestResource entity)
        {
            var request = _mapper.Map<Request>(entity);
            return _requestRepository.Update(request);
        }

        public async Task<bool> UpdateStatus(Guid requestId, RequestStatus newStatus)
        {
            var request= await _requestRepository.GetById(requestId);
            if (request == null) 
                return false;
            request.Status = newStatus;
            var result= await _requestRepository.UpdateStatus(request);
            return result > 0;
        }

        public async Task<IEnumerable<RequestResource>> GetRequestsByUserId(Guid userId)
        {
            var requests = await _requestRepository.GetRequestsByUserId(userId);
            return _mapper.Map<IEnumerable<RequestResource>>(requests);
        }
    }
}
