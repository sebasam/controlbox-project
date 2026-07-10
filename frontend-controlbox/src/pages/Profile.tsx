import { useAuthStore } from '../store/authStore';
import { User } from 'lucide-react';

export const Profile = () => {
  const { user } = useAuthStore();

  if (!user) return null;

  return (
    <div className="max-w-2xl mx-auto mt-10">
      <div className="bg-white rounded-lg shadow-sm border p-8">
        <h2 className="text-2xl font-bold mb-6">Profile</h2>
        <div className="flex items-center space-x-6">
          <div className="bg-indigo-100 p-6 rounded-full border-4 border-indigo-200">
            <User className="h-16 w-16 text-indigo-600" />
          </div>
          <div>
            <h1 className="text-3xl font-bold text-gray-900">{user.userName}</h1>
            <p className="text-gray-600 mt-2">{user.email}</p>
          </div>
        </div>
      </div>
    </div>
  );
};