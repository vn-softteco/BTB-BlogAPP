import * as yup from 'yup'
import { useState } from 'react'
import { useForm, SubmitHandler, DefaultValues } from 'react-hook-form'
import { useNavigate } from 'react-router-dom'
import { CommentService } from '@/services'
import { AddCommentFormType } from '@/types'
import { yupResolver } from '@hookform/resolvers/yup'
import { AddCommentForm } from '@/components/Auth'
import { ROUTES } from '@/utils/constants'
import { useParams } from 'react-router';

const schema = yup.object().shape({
    text: yup
        .string()
        .required('Text is required')
        .max(1000, 'Text must be no more than 1000 characters'),
    blogPostId: yup
        .string()
        .required()
    })


const AddCommentPage = () => {
    const [loading, setLoading] = useState<boolean>(false)
    const navigate = useNavigate()
    const { id } = useParams();

    const defaultValues: DefaultValues<AddCommentFormType> = {
      text: '',
      blogPostId: id
    }
  
    const { handleSubmit, control } = useForm<AddCommentFormType>({
        defaultValues,
        resolver: yupResolver(schema),
    })

    const onSubmit: SubmitHandler<AddCommentFormType> = async (data) => {
      setLoading(true)
      
      const res = await CommentService.addComment(data)

      if (res.status === 200 && res.data.success) {
          setLoading(false)
          navigate(ROUTES.GET_BLOGPOST_BY_ID(id!))
      }
    }

    return (
      <>
          <AddCommentForm
              control={control}
              handleSubmit={handleSubmit}
              onSubmit={onSubmit}
              loading={loading}
          />
      </>
    )
}

export default AddCommentPage