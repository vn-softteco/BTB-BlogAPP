import { CSSProperties } from 'react'
import { Theme } from '@mui/material'
import { SystemStyleObject } from '@mui/system'

export type StyleProps =
  | SystemStyleObject<Theme>
  | ((theme: Theme) => SystemStyleObject<Theme>)
  | CSSProperties

export type Styles = {
  [key: string]: StyleProps
}
