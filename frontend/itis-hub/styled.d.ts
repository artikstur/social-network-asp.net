import 'styled-components';
import { Theme } from './src/utils/theme';

declare module 'styled-components' {
    export interface DefaultTheme extends Theme {
        // Добавьте необходимые свойства
        colors: {
            text: string;
            primary: string;
            secondary: string;
        };
        fonts: {
            logoSize: string;
            bodyFont: string;
        };
    }
}
