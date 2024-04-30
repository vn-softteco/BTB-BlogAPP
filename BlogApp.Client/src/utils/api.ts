/* eslint-disable react-hooks/exhaustive-deps */
import axios from 'axios'

import { TokenService } from '@/services'

export const API_URL = 'https://localhost:7264/'

export const API = axios.create({
  baseURL: API_URL,
})

API.interceptors.request.use(
  (config) => {
    const token = TokenService.getAccessToken()

    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`
    }
    return config
  },
  (error) => {
    return Promise.reject(error)
  }
)

API.interceptors.response.use(
  (res) => {
    return res
  },
  async (err) => {
    return Promise.reject(err)
  }
)
