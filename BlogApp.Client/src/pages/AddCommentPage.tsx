import * as yup from 'yup'
import { useState } from 'react'
import { useForm, SubmitHandler, DefaultValues } from 'react-hook-form'
import { useNavigate } from 'react-router-dom'
import { CommentService } from '@/services'
import { AddCommentFormType, ApiError } from '@/types'
import { yupResolver } from '@hookform/resolvers/yup'
import { AddCommentForm } from '@/components/Auth'
import { ROUTES } from '@/utils/constants'
import { useParams } from 'react-router'
import { DefaultLayout } from '@/layouts/DefaultLayout'
import { getApiErrorMsg } from '@/utils/error.utils'

const schema = yup.object().shape({
    text: yup
        .string()
        .required('Text is required')
        .max(1000, 'Text must be no more than 1000 characters'),
    blogPostId: yup
        .string()
        .required()
    })

// TODO: Add navigation to previous page

const AddCommentPage = () => {
    const [loading, setLoading] = useState<boolean>(false)
    const navigate = useNavigate()
    const { id } = useParams();

    const defaultValues: DefaultValues<AddCommentFormType> = {
      text: '',
      blogPostId: id
    }
  
    const { handleSubmit, control, setError } = useForm<AddCommentFormType>({
        defaultValues,
        resolver: yupResolver(schema),
    })

    const onSubmit: SubmitHandler<AddCommentFormType> = async (data) => {
      setLoading(true)
      
      const res = await CommentService.addComment(data)

      try {
            if (res.status === 200 && res.data.success) {
                setLoading(false)
                navigate(ROUTES.GET_BLOGPOST_BY_ID(id!))
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
    }

    return (
      <DefaultLayout>
          <AddCommentForm
              control={control}
              handleSubmit={handleSubmit}
              onSubmit={onSubmit}
              loading={loading}
          />
      </DefaultLayout>
    )
}

export default AddCommentPage