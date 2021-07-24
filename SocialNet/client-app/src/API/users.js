import axios from 'axios'

const api = axios.create({
  baseURL: 'https://localhost:44390/api/users',
})

api.interceptors.request.use((requestConfig) => {
  const LOCAL_STORAGE_KEY = 'BearerToken'
  var token = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY))
  if (token) {
    requestConfig.headers = {
      Authorization: `Bearer ${token}`,
    };
  }
  return requestConfig;
})

export const signupUserHandler = (user) => {
  return api.post('/signup', user)
}

export const loginUserHandler = (userCreds) => {
  return api.post('/login', userCreds)
}

export const getUserInfo = (userid) => {
  return api.get(`/${userid}`)
}
