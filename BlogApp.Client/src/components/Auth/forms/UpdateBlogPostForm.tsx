import { Button, Box } from '@mui/material'
import { FormProps, UpdateBlogPostFormType } from '@/types'
import { TextInputController } from '@/components/Auth'

const AddBlogPostForm = ({
  control, 
  handleSubmit,
  onSubmit,
  loading = false
}: FormProps<UpdateBlogPostFormType>) => {
  return (
    <Box
      component="form"
      noValidate
      onSubmit={handleSubmit(onSubmit)}
    >
        <TextInputController
            label="Title"
            name="title"
            placeholder="Title"
            control={control}
            required
            sx={{ mb: 1.5 }}
        />

        <TextInputController
            label="Content"
            placeholder="Lorem Ipsum is simply dummy..."
            control={control}
            required
            sx={{ mb: 1.5 }}
            name="content"
        />

        <Button
            type="submit"
            size="large"
            fullWidth
            variant="contained"
            disabled={loading}
        >
            Update Blog Post
        </Button>
    </Box>
  )
}

export default AddBlogPostForm
