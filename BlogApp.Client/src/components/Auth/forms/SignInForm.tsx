import { Button, Box } from '@mui/material'
import { FormProps, SignInFormType } from '@/types'
import { TextInputController, PasswordController } from '@/components/Auth'

const SignInForm = ({
  control,
  handleSubmit,
  onSubmit,
  loading = false,
}: FormProps<SignInFormType>) => {
  return (
    <Box
      component="form"
      noValidate
      onSubmit={handleSubmit(onSubmit)}
    >
      <TextInputController
        label="Email"
        name="email"
        placeholder="your@email.com"
        control={control}
        required
        sx={{ mb: 1.5 }}
      />

      <PasswordController
        label="Password"
        placeholder=""
        control={control}
        required
        sx={{ mb: 1.5 }}
        name="password"
      />

      <Button
        type="submit"
        size="large"
        fullWidth
        variant="contained"
        disabled={loading}
      >
        Sing in
      </Button>
    </Box>
  )
}

export default SignInForm
