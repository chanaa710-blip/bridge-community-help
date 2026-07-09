using AutoMapper;
using Bridge.Core.Models;
using Bridge.Core.Repository;
using Bridge.Core.Resources;
using Bridge.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Service
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IRequestService _requestService;
        private readonly IMapper _mapper;
        public AnswerService(IAnswerRepository answerRepository, IRequestService requestService, IMapper mapper) 
        { 
            _answerRepository = answerRepository;
            _requestService = requestService;
            _mapper = mapper;
        }
        public async Task<Guid> Add(AnswerResource entity)
        {
            var answer = _mapper.Map<Answer>(entity);

            answer.Id = Guid.NewGuid();
            answer.CreatedAt = DateTime.Now;

            await _requestService.UpdateStatus(answer.RequestId, RequestStatus.InProgress);
            return await _answerRepository.Add(answer);
        }

        public async Task<IEnumerable<AnswerResource>> GetAll()
        {
            var answers = await _answerRepository.GetAll();
            return _mapper.Map<IEnumerable<AnswerResource>>(answers);
        }

        public Task<int> DeleteById(Guid id)
        {
            return (_answerRepository.DeleteById(id));
        }

        public async Task<IEnumerable<AnswerResource>> GetAnswersByRequestId(Guid requestId)
        {
            var answers=await _answerRepository.GetAnswersByRequestId(requestId);
            return _mapper.Map<IEnumerable<AnswerResource>>(answers);
        }

        public async Task<AnswerResource?> GetById(Guid id)
        {
            var answer=await _answerRepository.GetById(id);
            return _mapper.Map<AnswerResource>(answer);
        }

        public async Task<int> Update(AnswerResource entity)
        {
            var existingAnswer = await _answerRepository.GetById(entity.Id);

            if (existingAnswer == null)
            {
                throw new Exception("Answer not found");
            }
            _mapper.Map(entity, existingAnswer);

            return await _answerRepository.Update(existingAnswer);
        }

        public async Task<IEnumerable<AnswerResource>> GetAnswersByUserId(Guid userId)
        {
            var answers = await _answerRepository.GetAnswersByUserId(userId);
            return _mapper.Map<List<AnswerResource>>(answers)
                  .OrderBy(a => a.CreatedAt);
        }
    }
}
