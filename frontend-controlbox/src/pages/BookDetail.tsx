import { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { apiClient } from '../api/axios';
import { useAuthStore } from '../store/authStore';
import type { BookDetail as BookDetailType } from '../types';
import { Star, ArrowLeft } from 'lucide-react';

export const BookDetail = () => {
  const { id } = useParams<{ id: string }>();
  const [book, setBook] = useState<BookDetailType | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const { isAuthenticated } = useAuthStore();
  
  const [rating, setRating] = useState(5);
  const [comment, setComment] = useState('');
  const [submitting, setSubmitting] = useState(false);

  const fetchBook = async () => {
    try {
      const { data } = await apiClient.get<BookDetailType>(`/books/${id}`);
      setBook(data);
    } catch (err: any) {
      setError(err.response?.data?.error || 'Error loading book');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchBook();
  }, [id]);

  const handleSubmitReview = async (e: React.FormEvent) => {
    e.preventDefault();
    setSubmitting(true);
    try {
      await apiClient.post('/reviews', { bookId: id, rating, comment });
      setComment('');
      setRating(5);
      await fetchBook();
    } catch (err: any) {
      alert(err.response?.data?.error || 'Failed to submit review');
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) return <div className="text-center py-10">Loading...</div>;
  if (error || !book) return <div className="text-center py-10 text-red-600">{error || 'Not found'}</div>;

  return (
    <div className="max-w-4xl mx-auto">
      <Link to="/" className="flex items-center text-indigo-600 mb-6 hover:underline">
        <ArrowLeft className="h-4 w-4 mr-1" />
        Back to books
      </Link>
      
      <div className="bg-white rounded-lg shadow-sm border p-8 mb-8">
        <span className="text-sm font-semibold text-indigo-600 bg-indigo-50 px-3 py-1 rounded-full">
          {book.categoryName}
        </span>
        <h1 className="text-3xl font-bold text-gray-900 mt-4">{book.title}</h1>
        <p className="text-xl text-gray-600 mt-2">by {book.author}</p>
        <div className="mt-6">
          <h2 className="text-lg font-semibold mb-2">Summary</h2>
          <p className="text-gray-700 leading-relaxed">{book.summary}</p>
        </div>
      </div>

      <div className="bg-white rounded-lg shadow-sm border p-8">
        <h2 className="text-2xl font-bold mb-6">Reviews</h2>
        
        {isAuthenticated ? (
          <form onSubmit={handleSubmitReview} className="mb-8 border-b pb-8">
            <h3 className="text-lg font-semibold mb-4">Write a review</h3>
            <div className="mb-4">
              <label className="block text-gray-700 text-sm font-bold mb-2">Rating</label>
              <select
                value={rating}
                onChange={(e) => setRating(Number(e.target.value))}
                className="w-32 p-2 border rounded focus:outline-none focus:ring-2 focus:ring-indigo-500"
              >
                {[5, 4, 3, 2, 1].map((num) => (
                  <option key={num} value={num}>{num} Stars</option>
                ))}
              </select>
            </div>
            <div className="mb-4">
              <label className="block text-gray-700 text-sm font-bold mb-2">Comment</label>
              <textarea
                value={comment}
                onChange={(e) => setComment(e.target.value)}
                className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-indigo-500"
                rows={3}
                required
              />
            </div>
            <button
              type="submit"
              disabled={submitting}
              className="bg-indigo-600 text-white font-bold py-2 px-4 rounded hover:bg-indigo-700 disabled:opacity-50"
            >
              {submitting ? 'Submitting...' : 'Submit Review'}
            </button>
          </form>
        ) : (
          <div className="bg-gray-50 p-4 rounded-lg mb-8 text-center">
            <p className="text-gray-600 mb-2">Log in to leave a review</p>
            <Link to="/login" className="inline-block bg-indigo-600 text-white px-4 py-2 rounded-md">
              Go to Login
            </Link>
          </div>
        )}

        <div className="space-y-6">
          {book.reviews.length === 0 ? (
            <p className="text-gray-500 italic">No reviews yet.</p>
          ) : (
            book.reviews.map((review) => (
              <div key={review.id} className="bg-gray-50 p-4 rounded-lg">
                <div className="flex justify-between items-start mb-2">
                  <div className="flex items-center">
                    <div className="font-semibold mr-2">{review.userName}</div>
                    <div className="flex text-yellow-400">
                      {[...Array(review.rating)].map((_, i) => (
                        <Star key={i} className="h-4 w-4 fill-current" />
                      ))}
                    </div>
                  </div>
                  <div className="text-sm text-gray-500">
                    {new Date(review.createdAt).toLocaleDateString()}
                  </div>
                </div>
                <p className="text-gray-700">{review.comment}</p>
              </div>
            ))
          )}
        </div>
      </div>
    </div>
  );
};