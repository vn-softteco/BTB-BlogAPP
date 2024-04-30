import * as yup from 'yup'
import { useEffect, useState } from 'react'
import { useForm, SubmitHandler, DefaultValues } from 'react-hook-form'
import { useLocation, useNavigate } from 'react-router-dom'
import { AuthService, TokenService } from '@/services'
import { SignInFormType } from '@/types'
import { yupResolver } from '@hookform/resolvers/yup'
import { getApiErrorMsg } from '@/utils/error.utils'
import { LoginForm } from '@/components/Auth'
import { ROUTES } from '@/utils/constants'

const schema = yup.object().shape({
    email: yup
      .string()
      .email()
      .required('Email is required'),
    password: yup
      .string()
      .required('Password is required'),
  })

  const defaultValues: DefaultValues<SignInFormType> = {
    email: '',
    password: '',
  }

const LoginPage = () => {
    const [loading, setLoading] = useState<boolean>(false)
    const navigate = useNavigate()    
    
    const { handleSubmit, control, setError } = useForm<SignInFormType>({
      defaultValues,
      resolver: yupResolver(schema),
    })

    const onSubmit: SubmitHandler<SignInFormType> = async (data) => {
      setLoading(true)

      try {
        const res = await AuthService.login(data)

        if (res.status === 200 && res.data.success) {
          TokenService.setToken(res.data.data.token)
          navigate(ROUTES.INITIAL_ROUTE, { replace: true })
        }

        } catch (error) {
          const errMsg = getApiErrorMsg(error)
          switch (errMsg) {
            case 'Incorrect password!':
              setError('password', {
                type: 'manual',
                message: errMsg,
              })
              break
            case "User doesn't exist!":
              setError('email', {
                type: 'manual',
                message: errMsg,
              })
              break
            default:
              console.error("smth went wrong")
              break
          }
      }

    setLoading(false)
  }

  return (
    <>
        <LoginForm
            control={control}
            handleSubmit={handleSubmit}
            onSubmit={onSubmit}
            loading={loading}
        />
    </>
  )
}

export default LoginPage