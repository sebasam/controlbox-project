import { useState } from 'react';
import { Link } from 'react-router-dom';
import { apiClient } from '../api/axios';

export const ForgotPassword = () => {
  const [email, setEmail] = useState('');
  const [token, setToken] = useState('');
  const [error, setError] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    try {
      const { data } = await apiClient.post('/auth/forgot-password', { email });
      setToken(data.token);
    } catch (err: any) {
      setError(err.response?.data?.error || 'An error occurred');
    }
  };

  return (
    <div className="max-w-md mx-auto mt-10 p-6 bg-white rounded-lg shadow-md">
      <h2 className="text-2xl font-bold text-center mb-6">Reset Password</h2>
      {error && <div className="bg-red-50 text-red-600 p-3 rounded mb-4">{error}</div>}
      
      {!token ? (
        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label className="block text-gray-700 text-sm font-bold mb-2">Email</label>
            <input
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-indigo-500"
              required
            />
          </div>
          <button type="submit" className="w-full bg-indigo-600 text-white font-bold py-2 px-4 rounded hover:bg-indigo-700">
            Get Reset Token
          </button>
        </form>
      ) : (
        <div className="text-center">
          <div className="bg-green-50 text-green-700 p-4 rounded mb-4 break-all">
            <p className="font-bold mb-2">Your Reset Token:</p>
            <code className="text-sm">{token}</code>
          </div>
          <p className="text-gray-600 mb-4">Copy this token and proceed to reset your password.</p>
          <Link to={`/reset-password?email=${email}`} className="block w-full bg-indigo-600 text-white font-bold py-2 px-4 rounded hover:bg-indigo-700">
            Proceed to Reset
          </Link>
        </div>
      )}
      <div className="mt-4 text-center">
        <Link to="/login" className="text-indigo-600 hover:underline">Back to Login</Link>
      </div>
    </div>
  );
};