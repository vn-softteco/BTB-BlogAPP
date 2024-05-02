import * as yup from 'yup'
import { useState } from 'react'
import { useForm, SubmitHandler, DefaultValues } from 'react-hook-form'
import { useNavigate, useLocation } from 'react-router-dom'
import { BlogPostService } from '@/services'
import { UpdateBlogPostFormType, BlogPostListView, Styles } from '@/types'
import { yupResolver } from '@hookform/resolvers/yup'
import { getApiErrorMsg } from '@/utils/error.utils'
import { ROUTES } from '@/utils/constants'
import { UpdateBlogPostForm } from '@/components/Auth'
import { DefaultLayout } from '@/layouts/DefaultLayout'
import { Box, Button} from '@mui/material'

const schema = yup.object().shape({
    title: yup
      .string()
      .required('Title is required')
      .max(100, 'Title must be no more than 100 characters'),
    content: yup.string()
      .required('content is required')
      .max(1000, "Content must be no more than 1000 characters")
  })

const styles: Styles = {
    mainbox: {
        mt: 10,
    },
    goBackButton: {
        mb: 2,
    }
}

const UpdateBlogPostPage = () => {
    const [loading, setLoading] = useState<boolean>(false)
    const navigate = useNavigate()
    const { state } = useLocation()
    const blogPost = {...state} as BlogPostListView

    const defaultValues: DefaultValues<UpdateBlogPostFormType> = {
        title: blogPost.title,
        content: blogPost.content,
        id:  blogPost.id,
    }

    const isUpdate = blogPost && !!blogPost.id
  
    const { handleSubmit, control } = useForm<UpdateBlogPostFormType>({
        defaultValues,
        resolver: yupResolver(schema),
    })

    const onSubmit: SubmitHandler<UpdateBlogPostFormType> = async (data) => {
        setLoading(true)

        try {
            const res = isUpdate
                ? await BlogPostService.updateBlogPost(data)
                : await BlogPostService.addBlogPost(data)

            if (res.status === 200 && res.data.success) {
                navigate(ROUTES.GET_BLOGPOSTs, { replace: true })        
            }

        } catch (error) {
            const errMsg = getApiErrorMsg(error)
            switch (errMsg) {
                // TODO: error handling
            }
        }

        setLoading(false)
    }

  return (
    <DefaultLayout>
        <Box>
            <Button
                sx={styles.goBackButton}
                variant="contained"
                color='primary'     
                onClick={() => navigate(ROUTES.GET_BLOGPOSTs)}
            >
                Go back
            </Button>
            <UpdateBlogPostForm
                control={control}
                handleSubmit={handleSubmit}
                onSubmit={onSubmit}
                loading={loading}
            />
        </Box>
    </DefaultLayout>
  )
}

export default UpdateBlogPostPage