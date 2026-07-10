import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { apiClient } from '../api/axios';
import type { Book, Category } from '../types';
import { Search, Filter } from 'lucide-react';

export const Home = () => {
  const [books, setBooks] = useState<Book[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedCategory, setSelectedCategory] = useState('');
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const { data } = await apiClient.get<Category[]>('/categories');
        setCategories(data);
      } catch (error) {
        console.error('Failed to fetch categories', error);
      }
    };
    fetchCategories();
  }, []);

  useEffect(() => {
    const fetchBooks = async () => {
      setLoading(true);
      try {
        const params = new URLSearchParams();
        if (searchTerm) params.append('searchTerm', searchTerm);
        if (selectedCategory) params.append('categoryId', selectedCategory);
        
        const query = params.toString() ? `?${params.toString()}` : '';
        const { data } = await apiClient.get<Book[]>(`/books${query}`);
        setBooks(data);
      } catch (error) {
        console.error('Failed to fetch books', error);
      } finally {
        setLoading(false);
      }
    };

    const debounce = setTimeout(fetchBooks, 300);
    return () => clearTimeout(debounce);
  }, [searchTerm, selectedCategory]);

  return (
    <div>
      <div className="mb-8 flex flex-col sm:flex-row gap-4">
        <div className="relative flex-1">
          <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
            <Search className="h-5 w-5 text-gray-400" />
          </div>
          <input
            type="text"
            className="block w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg focus:ring-indigo-500 focus:border-indigo-500"
            placeholder="Search books by title or author..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>
        <div className="relative w-full sm:w-64">
          <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
            <Filter className="h-5 w-5 text-gray-400" />
          </div>
          <select
            className="block w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg focus:ring-indigo-500 focus:border-indigo-500 bg-white appearance-none"
            value={selectedCategory}
            onChange={(e) => setSelectedCategory(e.target.value)}
          >
            <option value="">All Categories</option>
            {categories.map((category) => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
          </select>
        </div>
      </div>

      {loading ? (
        <div className="text-center py-10">Loading books...</div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {books.map((book) => (
            <Link
              key={book.id}
              to={`/book/${book.id}`}
              className="bg-white rounded-lg shadow-sm border p-6 hover:shadow-md transition-shadow"
            >
              <span className="text-xs font-semibold text-indigo-600 bg-indigo-50 px-2 py-1 rounded-full">
                {book.categoryName}
              </span>
              <h3 className="mt-4 text-xl font-bold text-gray-900">{book.title}</h3>
              <p className="text-gray-600 mt-1">by {book.author}</p>
              <p className="mt-4 text-gray-500 line-clamp-3">{book.summary}</p>
            </Link>
          ))}
          {books.length === 0 && (
            <div className="col-span-full text-center py-10 text-gray-500">
              No books found matching your search.
            </div>
          )}
        </div>
      )}
    </div>
  );
};