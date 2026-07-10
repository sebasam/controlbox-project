import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { apiClient } from '../api/axios';
import { useAuthStore } from '../store/authStore';
import type { UserProfile } from '../types';
import { User, Star, BookOpen } from 'lucide-react';

export const Profile = () => {
  const { user } = useAuthStore();
  const [profile, setProfile] = useState<UserProfile | null>(null);
  const [pictureUrl, setPictureUrl] = useState('');
  const [isUpdating, setIsUpdating] = useState(false);

  const fetchProfile = async () => {
    try {
      const { data } = await apiClient.get<UserProfile>('/profile');
      setProfile(data);
      setPictureUrl(data.profilePictureUrl || '');
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    fetchProfile();
  }, []);

  const handleUpdatePicture = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsUpdating(true);
    try {
      await apiClient.put('/profile/picture', { pictureUrl });
      await fetchProfile();
    } catch (error) {
      console.error(error);
    } finally {
      setIsUpdating(false);
    }
  };

  if (!profile) return <div className="text-center py-10">Loading profile...</div>;

  return (
    <div className="max-w-4xl mx-auto mt-10">
      <div className="bg-white rounded-lg shadow-sm border p-8 mb-8">
        <h2 className="text-2xl font-bold mb-6">Profile</h2>
        <div className="flex flex-col md:flex-row items-start md:items-center space-y-6 md:space-y-0 md:space-x-8">
          <div className="flex-shrink-0">
            {profile.profilePictureUrl ? (
              <img src={profile.profilePictureUrl} alt="Profile" className="h-32 w-32 rounded-full object-cover border-4 border-indigo-100" />
            ) : (
              <div className="bg-indigo-100 p-8 rounded-full border-4 border-indigo-200">
                <User className="h-16 w-16 text-indigo-600" />
              </div>
            )}
          </div>
          <div className="flex-1 w-full">
            <h1 className="text-3xl font-bold text-gray-900">{profile.userName}</h1>
            <p className="text-gray-600 mt-1 mb-6">{profile.email}</p>
            
            <form onSubmit={handleUpdatePicture} className="flex gap-2 w-full max-w-md">
              <input
                type="url"
                placeholder="Profile Picture URL"
                value={pictureUrl}
                onChange={(e) => setPictureUrl(e.target.value)}
                className="flex-1 p-2 border rounded focus:outline-none focus:ring-2 focus:ring-indigo-500"
              />
              <button
                type="submit"
                disabled={isUpdating}
                className="bg-indigo-600 text-white px-4 py-2 rounded hover:bg-indigo-700 disabled:opacity-50"
              >
                Update
              </button>
            </form>
          </div>
        </div>
      </div>

      <div className="bg-white rounded-lg shadow-sm border p-8">
        <h2 className="text-2xl font-bold mb-6">My Reviews</h2>
        {profile.reviews.length === 0 ? (
          <p className="text-gray-500 italic">You haven't written any reviews yet.</p>
        ) : (
          <div className="space-y-6">
            {profile.reviews.map((review) => (
              <div key={review.id} className="bg-gray-50 p-4 rounded-lg">
                <div className="flex justify-between items-start mb-3">
                  <div>
                    <Link to={`/book/${review.bookId}`} className="text-lg font-bold text-indigo-600 hover:underline flex items-center">
                      <BookOpen className="h-4 w-4 mr-2" />
                      {review.bookTitle}
                    </Link>
                    <div className="flex text-yellow-400 mt-1">
                      {[...Array(review.rating)].map((_, i) => (
                        <Star key={i} className="h-4 w-4 fill-current" />
                      ))}
                    </div>
                  </div>
                  <span className="text-sm text-gray-500">
                    {new Date(review.createdAt).toLocaleDateString()}
                  </span>
                </div>
                <p className="text-gray-700">{review.comment}</p>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};