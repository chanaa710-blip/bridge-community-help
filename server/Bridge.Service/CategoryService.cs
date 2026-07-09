using AutoMapper;
using Bridge.Core.Models;
using Bridge.Core.Repository;
using Bridge.Core.Resources;
using Bridge.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bridge.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRequestRepository _requestRepository; 
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IRequestRepository requestRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Add(CategoryResource entity)
        {
            var category = _mapper.Map<Category>(entity);
            return await _categoryRepository.Add(category);
        }

        public async Task<IEnumerable<CategoryResource>> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryResource>>(categories);
        }

        public async Task<CategoryResource?> GetById(Guid id)
        {
            var category = await _categoryRepository.GetById(id);
            return _mapper.Map<CategoryResource>(category);
        }

        public async Task<CategoryResource> GetByNameAsync(string name)
        {
            var category = await _categoryRepository.GetByNameAsync(name);
            return _mapper.Map<CategoryResource>(category);
        }

        public async Task<IEnumerable<RequestResource>> GetRequestsByCategoryId(Guid categoryId)
        {
            var requests = await _categoryRepository.GetRequestsByCategoryId(categoryId);
            return _mapper.Map<IEnumerable<RequestResource>>(requests);
        }

        public async Task<int> Update(CategoryResource entity)
        {
            var category = _mapper.Map<Category>(entity);
            return await _categoryRepository.Update(category);
        }

        public async Task<int> DeleteById(Guid id)
        {
            return await _categoryRepository.DeleteById(id);
        }
    }
}