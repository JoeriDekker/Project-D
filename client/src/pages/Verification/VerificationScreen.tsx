import React, { useEffect } from 'react'
import { useParams } from 'react-router-dom';
import axios from 'axios';

function VerificationScreen() {
	const { userId, token } = useParams();
	useEffect(() => {
		async function handleVerification() {
			try {
				const res = await axios.get(process.env.REACT_APP_API_URL + '/api/register/confirm?userId=' + userId + '&token=' + token);
				window.location.href = '/login?success=verf';
			} catch (e) {
				console.log(e);
			}

		}
		handleVerification();
	});
  return (
    <div>VerificationScreen</div>
  )
}

export default VerificationScreen