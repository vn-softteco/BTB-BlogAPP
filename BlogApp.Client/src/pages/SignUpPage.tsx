import * as yup from 'yup'
import { useState } from 'react'
import { useForm, SubmitHandler, DefaultValues } from 'react-hook-form'
import { useNavigate } from 'react-router-dom'
import { AuthService, TokenService } from '@/services'
import { SignUpFormType, ApiError } from '@/types'
import { yupResolver } from '@hookform/resolvers/yup'
import { getApiErrorMsg } from '@/utils/error.utils'
import { SignUpForm } from '@/components/Auth'
import { ROUTES } from '@/utils/constants'

const schema = yup.object().shape({
    email: yup
        .string()
        .email()
        .required('Email is required'),
    password: yup
        .string()
        .required('Password is required'),
    firstName: yup
        .string()
        .required('First Name is required'),
    lastName: yup
        .string()
        .required('Last Name is required'),
  })

  const defaultValues: DefaultValues<SignUpFormType> = {
    email: '',
    password: '',
    firstName: '',
    lastName: '',
  }
  

const SignUpPage = () => {
    const [loading, setLoading] = useState<boolean>(false)
    const navigate = useNavigate()
  
    const { handleSubmit, control, setError } = useForm<SignUpFormType>({
      defaultValues,
      resolver: yupResolver(schema),
    })

    const onSubmit: SubmitHandler<SignUpFormType> = async (data) => {
      setLoading(true)

      try {
        const res = await AuthService.signup(data)

        if (res.status === 200 && res.data.success) {
          navigate(ROUTES.SIGN_IN)
        }

      } catch (error) {
        const errors = getApiErrorMsg(error)
        if(!!errors){
          errors.map((err: ApiError) => {
            err.name.map((name: string) => {
              setError(err.key.toLocaleLowerCase(), {
                type: 'manual',
                message: name,
              })
            })
          })
        }
      }

      setLoading(false)
    }

  return (
    <>
      <SignUpForm
          control={control}
          handleSubmit={handleSubmit}
          onSubmit={onSubmit}
          loading={loading}
      />
    </>
  )
}

export default SignUpPage