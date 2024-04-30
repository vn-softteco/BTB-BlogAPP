import { API_ENDPOINTS } from '@/utils/constants'
import { API } from '@/utils/api'

import {
    SignInFormType,
    SignUpFormType
  } from '@/types/formTypes'

type LoginResponse = {
    errorMessage: string | null
    success: boolean
    data: {
      token: string
      userId: string
    }
  }

  const login = async (data: SignInFormType) => {
    return await API.post<LoginResponse>(API_ENDPOINTS.SIGN_IN, {
      email: data.email,
      password: data.password,
    })
  }

  const signup = async (data: SignUpFormType) => {
    return await API.post(API_ENDPOINTS.SIGN_UP, data)
  }

  export default {
    login,
    signup
  }