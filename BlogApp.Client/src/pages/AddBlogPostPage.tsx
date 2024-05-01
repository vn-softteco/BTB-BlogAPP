import * as yup from 'yup'
import { useState } from 'react'
import { useForm, SubmitHandler, DefaultValues } from 'react-hook-form'
import { useNavigate } from 'react-router-dom'
import { BlogPostService } from '@/services'
import { AddBlogPostFormType, ApiError } from '@/types'
import { yupResolver } from '@hookform/resolvers/yup'
import { getApiErrorMsg } from '@/utils/error.utils'
import { ROUTES } from '@/utils/constants'
import { AddBlogPostForm } from '@/components/Auth'
import { DefaultLayout } from '@/layouts/DefaultLayout'

const schema = yup.object().shape({
    title: yup
      .string()
      .required('Title is required')
      .max(100, 'Title must be no more than 100 characters'),
    content: yup.string()
      .required('content is required')
      .max(5000, "Content must be no more than 5000 characters")
  })
  

const AddBlogPostPage = () => {
    const [loading, setLoading] = useState<boolean>(false)
    const navigate = useNavigate()

    const defaultValues: DefaultValues<AddBlogPostFormType> = {
        title: '',
        content: ''
    }
  
    const { handleSubmit, control, setError } = useForm<AddBlogPostFormType>({
        defaultValues,
        resolver: yupResolver(schema),
    })

    const onSubmit: SubmitHandler<AddBlogPostFormType> = async (data) => {
        setLoading(true)

        try {
            const res = await BlogPostService.addBlogPost(data)

            if (res.status === 200 && res.data.success) {
                navigate(ROUTES.GET_BLOGPOSTs, { replace: true })        
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
    <DefaultLayout>
        <AddBlogPostForm
            control={control}
            handleSubmit={handleSubmit}
            onSubmit={onSubmit}
            loading={loading}
        />
    </DefaultLayout>
  )
}

export default AddBlogPostPage