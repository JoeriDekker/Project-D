import React from 'react'
import useSignOut from 'react-auth-kit/hooks/useSignOut';

function Dashboard() {
  const signOut = useSignOut()
  function handleLogout() {
    signOut()
    window.location.href = '/login'
  }
  return (
    <div>
      <button className='bg-red-400 p-4 text-white' onClick={handleLogout}>Uitloggen</button>
    </div>
  )
}

export default Dashboard