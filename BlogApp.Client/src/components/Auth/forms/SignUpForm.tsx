import { Button, Box } from '@mui/material'
import { FormProps, SignUpFormType } from '@/types'
import { TextInputController, PasswordController } from '@/components/Auth'

const SignInForm = ({
  control,
  handleSubmit,
  onSubmit,
  loading = false,
}: FormProps<SignUpFormType>) => {
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

        <TextInputController
            label="FirstName"
            name="firstName"
            placeholder="First Name"
            control={control}
            required
            sx={{ mb: 1.5 }}
        />

        <TextInputController
            label="LastName"
            name="lastName"
            placeholder="Last Name"
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
        Sing up
      </Button>
    </Box>
  )
}

export default SignInForm
