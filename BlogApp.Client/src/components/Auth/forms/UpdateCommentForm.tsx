import { Button, Box } from '@mui/material'
import { FormProps, UpdateCommentFormType } from '@/types'
import { TextInputController } from '@/components/Auth'

const AddCommentForm = ({
  control,
  handleSubmit,
  onSubmit,
  loading = false,
}: FormProps<UpdateCommentFormType>) => {
  return (
    <Box
      component="form"
      noValidate
      onSubmit={handleSubmit(onSubmit)}
    >
      <TextInputController
        label="Text"
        name="text"
        placeholder="Text"
        control={control}
        required
        sx={{ mb: 1.5 }}
      />
      
      <Button
        type="submit"
        size="large"
        fullWidth
        variant="contained"
        disabled={loading}
      >
        Update comment
      </Button>
    </Box>
  )
}

export default AddCommentForm
