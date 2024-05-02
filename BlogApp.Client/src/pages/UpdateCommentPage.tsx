import * as yup from 'yup'
import { useState } from 'react'
import { useForm, SubmitHandler, DefaultValues } from 'react-hook-form'
import { useNavigate, useLocation } from 'react-router-dom'
import { CommentService } from '@/services'
import { UpdateCommentFormType, Comment, Styles } from '@/types'
import { yupResolver } from '@hookform/resolvers/yup'
import { UpdateCommentForm } from '@/components/Auth'
import { ROUTES } from '@/utils/constants'
import { useParams } from 'react-router'
import { DefaultLayout } from '@/layouts/DefaultLayout'
import { Box, Button} from '@mui/material'

const schema = yup.object().shape({
    text: yup
        .string()
        .required('Text is required')
        .max(1000, 'Text must be no more than 1000 characters'),
    blogPostId: yup
        .string()
        .required(),
    id: yup.string().required()
  })

const styles: Styles = {
    mainbox: {
        mt: 10,
    },
    goBackButton: {
        mb: 2,
    }
}


const AddOrUpdateCommentPage = () => {
    const [loading, setLoading] = useState<boolean>(false)
    const navigate = useNavigate()
    const { id } = useParams();
    const { state } = useLocation();
    const comment = {...state} as Comment

    const defaultValues: DefaultValues<UpdateCommentFormType> = {
        id: comment.id,
        text: comment?.text || '',
        blogPostId: id
    }
  
    const { handleSubmit, control } = useForm<UpdateCommentFormType>({
        defaultValues,
        resolver: yupResolver(schema),
    })

    const onSubmit: SubmitHandler<UpdateCommentFormType> = async (data) => {
        setLoading(true)
        
        const res = await CommentService.updateComment(data)

        if (res.status === 200 && res.data.success) {
            setLoading(false)
            navigate(ROUTES.GET_BLOGPOST_BY_ID(id!))
        }
    }

    return (
      <DefaultLayout>
        <Box>
            <Button
                sx={styles.goBackButton}
                variant="contained"
                color='primary'     
                onClick={() => navigate(ROUTES.GET_BLOGPOST_BY_ID(id!))}
            >
                Go back
            </Button>
            <UpdateCommentForm
                control={control}
                handleSubmit={handleSubmit}
                onSubmit={onSubmit}
                loading={loading}
            />
          </Box>
      </DefaultLayout>
    )
}

export default AddOrUpdateCommentPage